<template>
  <div class="container-fluid min-dvh-100 main-bg">
    <div class="main-content row justify-content-center">
      <header class="mt-5 top-0 w-100 text-center mb-5 mb-lg-0">
        <img class="d-none d-lg-inline-flex" src="../images/title.webp">
        <img class="d-lg-none" src="../images/title-m.webp">
        <h5>Vota da smartphone la parola che rappresenta al meglio questa sessione​</h5>
      </header>

      <div v-if="!hasVoted" class="col-12 col-lg-10 pb-5 pb-lg-0">
        <div v-if="questionData"
          class="row justify-content-center row-cols-2 row-cols-lg-auto g-lg-4 g-3 row-gap-4 pb-5 pb-lg-0">
          <!-- <h2>{{ questionData.questionText }}</h2>
        Passiamo a SessionForm i 10 termini recuperati -->
          <SessionForm :sessionId="sessionId" :terms="questionData.questionAnswers" @voted="handleVoted" />
        </div>
        <div v-else>
          <p>Caricamento in corso...</p>
        </div>
      </div>
      <div v-else>
        <h1 class="text-secondary text-center fw-bold mb-5">Hai già votato​</h1>
        <h2 class="text-secondary text-center mb-5">... attendi i risultati</h2>
      </div>
      <QrCodeDisplay :sessionId="sessionId" />

      <!-- <a :href="`/session/${nextSessionId}`"
        class="position-absolute bottom-0 mb-4 end-0 btn fs-1 me-5 w-auto d-lg-block d-none"><i
          class="bi bi-arrow-right-circle text-secondary"></i></a> -->

      <button class="position-absolute bottom-0 mb-4 end-0 btn fs-1 me-5 w-auto d-lg-block d-none"
        @click="handleNextSessionId"><i class="bi bi-arrow-right-circle text-secondary"></i></button>

    </div>
  </div>
</template>

<script>
import SessionForm from '@/components/SessionForm.vue'
import QrCodeDisplay from '@/components/QrCodeDisplay.vue'
import { useRoute, useRouter } from 'vue-router'
import { defineComponent, onMounted, computed, ref, watch } from 'vue'
import { useSessionsStore } from '@/store/sessions'
import { useQuestionsStore } from '@/store/questions'
//import config from "@/config.js";

export default defineComponent({
  name: 'SessionView',
  components: {
    SessionForm,
    QrCodeDisplay
  },
  setup() {
    const route = useRoute();
    const router = useRouter();
    const questionsStore = useQuestionsStore();
    const sessionsStore = useSessionsStore();
    const hasVoted = ref(false);

    //const sessionId = route.params.sessionId;
    // sessionId diventa un computed che dipende da route.params.sessionId
    const sessionId = computed(() => route.params.sessionId);

    // Carichiamo la domanda non appena entriamo nella view
    onMounted(async () => {
      console.log("onMounted")
      await questionsStore.loadQuestions();
      //await questionsStore.loadQuestion(sessionId);
      hasVoted.value = sessionsStore.hasVoted(sessionId.value);
    })

    // Aggiungi un watcher per aggiornare hasVoted quando sessionId cambia
    watch(sessionId, (newSessionId) => {
      hasVoted.value = sessionsStore.hasVoted(newSessionId);
    });

    const handleVoted = async (wordId, answerText) => {
      console.log('voted:', sessionId.value, wordId, answerText);
      await sessionsStore.addWordToSession(sessionId.value, wordId, answerText);
      router.push({ path: `/thankyou/${sessionId.value}` });
    }

    // questionData è un computed che ritorna il record salvato nello store
    const questionData = computed(() => {
      //return questionsStore.questionsMap[config.CURRENT_MEETING_ID][sessionId]
      return questionsStore.getQuestionById(sessionId.value);
    })

    const nextSessionId = computed(() => {
      return questionsStore.nextSessionId(sessionId.value);
      // let currentId = parseInt(sessionId) + 1;
      // if (currentId > 3) {
      //   currentId = 1;
      // }
      // return currentId;
    });

    const handleNextSessionId = () => {
      const nextSessionId = questionsStore.nextSessionId(sessionId.value);
      console.log(nextSessionId);
      router.push({ path: `/session/${nextSessionId}` });
    }
    return {
      sessionId,
      questionData,
      hasVoted,
      handleVoted,
      nextSessionId,
      handleNextSessionId
    }
  }
})
</script>
