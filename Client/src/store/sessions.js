import { defineStore } from 'pinia'
import config from '@/config.js' // Importiamo la configurazione

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
        const response = await fetch(`${config.API_BASE_URL}/Votes`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(vote)
        })

        if (!response.ok) {
          throw new Error('Errore nell’invio del voto')
        }

        this.sessionsData[sessionId].push(word)
      } catch (error) {
        console.error('Errore:', error)
      }
    },

    async loadVotesFromApi() {
      try {
        const response = await fetch(`${config.API_BASE_URL}/Votes`)
        if (!response.ok) {
          throw new Error('Errore nel recupero dei voti')
        }

        const data = await response.json()

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
