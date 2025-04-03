// src/store/sessions.js

import { defineStore } from "pinia";
import config from "@/config.js";

export const useSessionsStore = defineStore("sessions", {
	state: () => ({
		allVotes: [],
	}),

	actions: {
		/**
		 * Verifica in localStorage se l'utente ha già votato
		 * per la sessionId specificata.
		 */
		hasVoted(sessionId) {
			return localStorage.getItem(`hasVoted_${sessionId}`) === "true";
		},

		/**
		 * Segna in localStorage che l'utente ha votato
		 * per la sessionId specificata.
		 */
		markVoted(sessionId) {
			localStorage.setItem(`hasVoted_${sessionId}`, "true");
		},
		/**
		 * Invia il voto al backend.
		 * Se l'utente ha già votato, esce subito.
		 */
		async addWordToSession(questionId, answerId, answerText) {
			// Controlla se l'utente ha già votato questa sessione
			if (this.hasVoted(questionId)) {
				console.warn(`Hai già votato la sessione con ID ${questionId}`);
				return;
			}

			const vote = {
				questionId: parseInt(questionId),
				questionAnswerId: parseInt(answerId),
				questionAnswerText: answerText,
				meetingId: config.CURRENT_MEETING_ID,
			};
			//console.log(vote);
			try {
				await fetch(`${config.API_BASE_URL}/votes`, {
					method: "POST",
					headers: { "Content-Type": "application/json" },
					body: JSON.stringify(vote),
				});

      // Se la chiamata ha successo, marca la sessione come votata
      this.markVoted(questionId);

			} catch (error) {
				console.error("Errore:", error);
			}
		},

		async loadAllVotesFromApi() {
			try {
				const response = await fetch(`${config.API_BASE_URL}/votes/${config.CURRENT_MEETING_ID}`);
				if (!response.ok) {
					throw new Error("Errore nel recupero dei voti");
				}
				this.allVotes = await response.json();
				//console.log(this.allVotes)
			} catch (error) {
				console.error("Errore:", error);
			}
		},
	},

	getters: {
		getVotesByQuestionId: (state) => (sessionId) => {
			return state.allVotes.filter((v) => v.questionId === Number(sessionId));
		},
		getAllVotes: (state) => {
			return state.allVotes; //.map((v) => v.word);
		},
	},
});
