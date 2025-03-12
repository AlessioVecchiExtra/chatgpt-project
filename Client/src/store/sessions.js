import { defineStore } from 'pinia'

export const useSessionsStore = defineStore('sessions', {
  state: () => ({
    sessionsData: {
      1: [],
      2: [],
      3: []
    }
  }),
  actions: {
    async addWordToSession(sessionId, word) {
      const vote = {
        sessionId: parseInt(sessionId),
        word: word
      }

      try {
        const response = await fetch('http://localhost:5184/api/Votes', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(vote)
        })

        if (!response.ok) {
          throw new Error('Errore nell’invio del voto')
        }

        // Aggiorniamo lo stato locale solo se la richiesta ha successo
        this.sessionsData[sessionId].push(word)
      } catch (error) {
        console.error('Errore:', error)
      }
    },

    async loadVotesFromApi() {
      try {
        const response = await fetch('http://localhost:5184/api/Votes')
        if (!response.ok) {
          throw new Error('Errore nel recupero dei voti')
        }

        const data = await response.json()

        // Resetta la sessione prima di popolarla di nuovo
        this.sessionsData = { 1: [], 2: [], 3: [] }

        data.forEach(vote => {
          this.sessionsData[vote.sessionId].push(vote.word)
        })
      } catch (error) {
        console.error('Errore:', error)
      }
    }
  }
})
