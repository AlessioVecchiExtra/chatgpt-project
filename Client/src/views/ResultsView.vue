<template>
  <div>
    <h1>Risultati</h1>

    <!-- Dropdown di selezione sessione -->
    <label for="sessionSelect">Sessione:</label>
    <select id="sessionSelect" v-model="selectedSession">
      <option value="all">Tutte</option>
      <option value="1">Sessione 1</option>
      <option value="2">Sessione 2</option>
      <option value="3">Sessione 3</option>
    </select>

    <!-- Mostra la word cloud con le parole filtrate -->
    <WordCloud :words="filteredWords" />
  </div>
</template>

<script>
import { defineComponent, onMounted, computed, ref, watch } from 'vue'
import { useSessionsStore } from '@/store/sessions'
import WordCloud from '@/components/WordCloud.vue'
import { useRouter, useRoute } from 'vue-router'

export default defineComponent({
  name: 'ResultsView',
  components: { WordCloud },
  // Grazie a 'props: true' nel router, sessionId arriverà qui
  props: {
    sessionId: {
      type: String,
      default: undefined
    }
  },
  setup(props) {
    const sessionsStore = useSessionsStore()

    const router = useRouter();
    
    const route = useRoute();
    const sessionId = route.params.id

    const selectedSession = ref(props.sessionId ?? 'all')

    // Se l’utente cambia la select, potresti fare un router push, se vuoi sincronizzare l'URL:
    watch(selectedSession, (newVal) => {
      if (newVal === 'all') {
        router.push('/results')
      } else {
        router.push(`/results/${newVal}`)
      }
    })

    // Carichiamo tutti i voti dal backend all'avvio
    onMounted(() => {
      sessionsStore.loadAllVotesFromApi()
    })

    // Calcola l'array di parole in base a selectedSession
    const filteredWords = computed(() => {
      if (selectedSession.value === 'all') {
        // Mostra tutti i voti (ricavati dallo store)
        return sessionsStore.getAllWords
      } else {
        // Filtra solo i voti della sessione selezionata
        const votes = sessionsStore.getVotesForSession(selectedSession.value)
        return votes.map(v => v.word)
      }
    })

    return {
      selectedSession,
      filteredWords
    }
  }
})
</script>
