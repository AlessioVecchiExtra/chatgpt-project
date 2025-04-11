<template>
  <div class="container-fluid">
    <div class="main-content row main-bg">
      <header class="mt-5 top-0 w-100 text-center">
        <img src="../images/title.webp">
      </header>
      <WordCloud :words="filteredWords" :minFontSize="25" :maxFontSize="60" />
    </div>

    <!-- <div class="mb-3 d-none">
      <label for="sessionSelect" class="form-label">Seleziona una sessione:</label>
      <select id="sessionSelect" v-model="selectedSession" class="form-select">
        <option value="all">Tutte</option>
        <option value="1">Sessione 1</option>
        <option value="2">Sessione 2</option>
        <option value="3">Sessione 3</option>
      </select>
    </div> -->

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
/*  props: {
    sessionId: {
      type: String,
      default: undefined
    }
  },*/
  setup(props) {
    const sessionsStore = useSessionsStore()
    const router = useRouter();
    const route = useRoute();
/*
    const selectedSession = ref(props.sessionId ?? 'all')
    // Se l’utente cambia la select, potresti fare un router push, se vuoi sincronizzare l'URL:
    watch(selectedSession, (newVal) => {
      if (newVal === 'all') {
        router.push('/results')
      } else {
        router.push(`/results/${newVal}`)
      }
    })
*/
    // Carichiamo tutti i voti dal backend all'avvio
    onMounted(() => {
      sessionsStore.loadVotes()
    })

    /**
     * Legge la query string ?sessionIds=1,2,3 oppure ?sessionIds[]=1&sessionIds[]=2
     * e la trasforma in array di numeri. Se non presente, restituisce array vuoto
     * (che tratteremo come "tutte le sessioni").
     */
    const sessionIds = computed(() => {
      const param = route.query.sessionIds

      if (!param) {
        // Nessun sessionIds => array vuoto (lo useremo come "all")
        return []
      }

      // Se param è un array (ad es. ?sessionIds[]=1&sessionIds[]=2)
      if (Array.isArray(param)) {
        return param.map(id => parseInt(id, 10))
      }

      // Altrimenti se è una stringa "1,2,3"
      return param
        .split(',')
        .map(id => parseInt(id.trim(), 10))
        .filter(n => !isNaN(n)) // filtra eventuali parse falliti
    })

    /**
      * filteredWords:
      *  - Se sessionIds è vuoto, mostriamo tutti i voti.
      *  - Altrimenti filtriamo i voti la cui questionId è inclusa in sessionIds.
      */
    const filteredWords = computed(() => {
      const allVotes = sessionsStore.getAllVotes
      const ids = sessionIds.value

      // Se è vuoto => "all"
      if (ids.length === 0) {
        return allVotes.map(v => v.questionAnswerText)
      }

      // Filtra i voti
      const filtered = allVotes.filter(v => ids.includes(v.questionId))
      return filtered.map(v => v.questionAnswerText)
    })


    // Calcola l'array di parole in base a selectedSession
    // const filteredWords = computed(() => {
    //   if (selectedSession.value === 'all') {
    //     // Mostra tutti i voti (ricavati dallo store)
    //     const votes = sessionsStore.getAllVotes
    //     const items = votes.map(v => v.questionAnswerText)
    //     return items;
    //   } else {
    //     // Filtra solo i voti della sessione selezionata
    //     const votes = sessionsStore.getVotesByQuestionId(selectedSession.value)
    //     const items = votes.map(v => v.questionAnswerText)
    //     return items;
    //   }
    // })

    return {
      //selectedSession,
      filteredWords
    }
  }
})
</script>
