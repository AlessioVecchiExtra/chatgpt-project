<template>
  <div>
    <h1>Sessione {{ sessionId }}</h1>

    <div v-if="questionData">
      <h2>{{ questionData.questionText }}</h2>
      <!-- Passiamo a SessionForm i 10 termini recuperati -->
      <SessionForm :sessionId="sessionId" :terms="questionData.terms" />
    </div>

    <div v-else>
      <p>Caricamento in corso...</p>
    </div>

    <QrCodeDisplay :sessionId="sessionId" />
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

    return {
      sessionId,
      questionData
    }
  }
})
</script>
