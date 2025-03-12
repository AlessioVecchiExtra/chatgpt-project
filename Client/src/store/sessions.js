// src/store/sessions.js

import { defineStore } from 'pinia'
import config from '@/config.js'

export const useSessionsStore = defineStore('sessions', {
  state: () => ({
    allVotes: []
  }),

  actions: {
    async addWordToSession(sessionId, word) {
      const vote = { sessionId: parseInt(sessionId), word }
      try {
        await fetch(`${config.API_BASE_URL}/Votes`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(vote)
        })
      } catch (error) {
        console.error('Errore:', error)
      }
    },

    async loadAllVotesFromApi() {
      try {
        const response = await fetch(`${config.API_BASE_URL}/Votes`)
        if (!response.ok) {
          throw new Error('Errore nel recupero dei voti')
        }
        this.allVotes = await response.json()
      } catch (error) {
        console.error('Errore:', error)
      }
    }
  },

  getters: {
    getVotesForSession: (state) => (sessionId) => {
      return state.allVotes.filter(v => v.sessionId === Number(sessionId))
    },
    getAllWords: (state) => {
      // Ritorna solo l'array di parole (da tutti i voti)
      return state.allVotes.map(v => v.word)
    }
  }
})
