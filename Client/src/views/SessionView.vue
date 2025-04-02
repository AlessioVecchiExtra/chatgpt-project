<template>
  <div class="container-fluid min-vh-100 main-bg">
    <div class="main-content row justify-content-center">
      <header class="mt-5 top-0 w-100 text-center mb-5 mb-lg-0">
        <img class="d-none d-lg-inline-flex" src="../images/title.webp">
        <img class="d-lg-none" src="../images/title-m.webp">
        <h5>Vota da smartphone la parola che rappresenta al meglio questa sessione​</h5>
      </header>
      
      <div v-if="questionData" class="col-12 col-lg-10">
        <!-- <h2>{{ questionData.questionText }}</h2>
        Passiamo a SessionForm i 10 termini recuperati -->
        <SessionForm :sessionId="sessionId" :terms="questionData.questionAnswers" />
      </div>
      <div v-else>
        <p>Caricamento in corso...</p>
      </div>
    
      <QrCodeDisplay :sessionId="sessionId" />
      
      <a :href="`/session/${nextSessionId()}`" class="position-absolute bottom-0 mb-4 end-0 btn fs-1 me-5 w-auto d-lg-block d-none"><i
          class="bi bi-arrow-right-circle text-secondary"></i></a>

    </div>
  </div>
</template>

<script>
import SessionForm from '@/components/SessionForm.vue'
import QrCodeDisplay from '@/components/QrCodeDisplay.vue'
import { useRoute } from 'vue-router'
import { defineComponent, onMounted, computed } from 'vue'
import { useQuestionsStore } from '@/store/questions'

export default defineComponent({
  name: 'SessionView',
  components: {
    SessionForm,
    QrCodeDisplay
  },
  setup() {
    const route = useRoute()
    const sessionId = route.params.id
    const questionsStore = useQuestionsStore()

    // Carichiamo la domanda non appena entriamo nella view
    onMounted(async () => {
      await questionsStore.loadQuestion(sessionId)
    })

    // questionData è un computed che ritorna il record salvato nello store
    const questionData = computed(() => {
      return questionsStore.questionsMap[sessionId]
    })

    const nextSessionId = () => {   
      let currentId = parseInt(sessionId) + 1;      
      if (currentId > 3) {
        currentId = 1;
      }      
      return currentId;
    }

    return {
      sessionId,
      questionData,
      nextSessionId
    }
  }
})
</script>
