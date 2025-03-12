import { defineStore } from 'pinia'
import config from '@/config.js'

export const useQuestionsStore = defineStore('questions', {
  state: () => ({
    // Mappa sessionId -> oggetto { questionText, terms: [] }
    questionsMap: {}
  }),
  actions: {
    async loadQuestion(sessionId) {
      try {
        // Esempio: GET /api/Questions/1
        const response = await fetch(`${config.API_BASE_URL}/Questions/${sessionId}`)
        if (!response.ok) {
          throw new Error('Errore nel recupero della domanda')
        }

        const data = await response.json() 
        // data ha la forma:
        // {
        //   "sessionId": 1,
        //   "questionText": "Quale parola rappresenta meglio la Sessione 1?",
        //   "terms": [ ...10 termini... ]
        // }

        // Salviamo i dati nello state
        this.questionsMap[sessionId] = {
          questionText: data.questionText,
          terms: data.terms
        }

      } catch (error) {
        console.error('Errore:', error)
      }
    }
  }
})
