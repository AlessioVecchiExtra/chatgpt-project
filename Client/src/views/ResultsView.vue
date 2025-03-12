<template>
  <div>
    <h1>Risultati</h1>
    <WordCloud :words="allWords" />
  </div>
</template>

<script>
import { useSessionsStore } from '@/store/sessions'
import WordCloud from '@/components/WordCloud.vue'
import { computed, onMounted } from 'vue'

export default {
  name: 'ResultsView',
  components: {
    WordCloud
  },
  setup() {
    const sessionsStore = useSessionsStore()

    const allWords = computed(() => {
      let combined = []
      Object.keys(sessionsStore.sessionsData).forEach(sessionId => {
        combined = combined.concat(sessionsStore.sessionsData[sessionId])
      })
      return combined
    })

    // Quando la pagina dei risultati viene aperta, carichiamo i dati
    onMounted(() => {
      sessionsStore.loadVotesFromApi()
    })

    return {
      allWords
    }
  }
}
</script>
