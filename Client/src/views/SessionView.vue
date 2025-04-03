<template>
  <div class="container-fluid min-dvh-100 main-bg">
    <div class="main-content row justify-content-center">
      <header class="mt-5 top-0 w-100 text-center mb-5 mb-lg-0">
        <img class="d-none d-lg-inline-flex" src="../images/title.webp">
        <img class="d-lg-none" src="../images/title-m.webp">
        <h5>Vota da smartphone la parola che rappresenta al meglio questa sessione​</h5>
      </header>

      <div v-if="!hasVoted">
        <div v-if="questionData" class="col-12 col-lg-10">
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
        <h2 class="text-secondary text-center mb-5">... attendi il referto ;-)</h2>
      </div>
      <QrCodeDisplay :sessionId="sessionId" />

      <a :href="`/session/${nextSessionId}`"
        class="position-absolute bottom-0 mb-4 end-0 btn fs-1 me-5 w-auto d-lg-block d-none"><i
          class="bi bi-arrow-right-circle text-secondary"></i></a>

    </div>
  </div>
</template>

<script>
import SessionForm from '@/components/SessionForm.vue'
import QrCodeDisplay from '@/components/QrCodeDisplay.vue'
import { useRoute, useRouter } from 'vue-router'
import { defineComponent, onMounted, computed, ref } from 'vue'
import { useSessionsStore } from '@/store/sessions'
import { useQuestionsStore } from '@/store/questions'

export default defineComponent({
  name: 'SessionView',
  components: {
    SessionForm,
    QrCodeDisplay
  },
  setup() {
    const route = useRoute();
    const router = useRouter();
    const sessionId = route.params.sessionId;
    const questionsStore = useQuestionsStore();
    const sessionsStore = useSessionsStore();
    const hasVoted = ref(false);

    // Carichiamo la domanda non appena entriamo nella view
    onMounted(async () => {
      await questionsStore.loadQuestion(sessionId);
      hasVoted.value = sessionsStore.hasVoted(sessionId);
    })

    const handleVoted = async (wordId, answerText) => {
      console.log('voted:', sessionId, wordId, answerText);
      await sessionsStore.addWordToSession(sessionId, wordId, answerText);
      router.push({ path: `/thankyou/${sessionId}` });
    }

    // questionData è un computed che ritorna il record salvato nello store
    const questionData = computed(() => {
      return questionsStore.questionsMap[sessionId]
    })

    const nextSessionId = computed(() => {
      return questionsStore.nextSessionId(sessionId);
      // let currentId = parseInt(sessionId) + 1;
      // if (currentId > 3) {
      //   currentId = 1;
      // }
      // return currentId;
    });

    return {
      sessionId,
      questionData,
      hasVoted,
      handleVoted,
      nextSessionId
    }
  }
})
</script>
