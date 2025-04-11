// src/store/sessions.js

import { defineStore } from "pinia";
import config from "@/config.js";

const VOTING_COOLDOWN_MINUTES = 3;

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
			//return localStorage.getItem(`hasVoted_${sessionId}`) === "true";

			// Recupero dal localStorage il valore salvato
			const storedData = localStorage.getItem(`hasVoted_${sessionId}`);

			// Se non c’è niente salvato, l’utente può votare (quindi hasVoted = false)
			if (!storedData) {
				return false;
			}

			// Altrimenti, recupero il timestamp in cui potrà votare di nuovo
			const { nextVoteTime } = JSON.parse(storedData);

			const now = Date.now();
			// Se l'ora attuale è minore di nextVoteTime, i 5 minuti non sono ancora passati
			// e l'utente NON può ancora votare
			return now < nextVoteTime;
		},

		/**
		 * Salva l'array di risposte e imposta il tempo di "cooldown"
		 * (da now + 5 minuti) per la sessione specificata.
		 */
		markVoted(sessionId, answerText) {
			// Leggi i dati esistenti, se presenti
			const storedData = localStorage.getItem(`hasVoted_${sessionId}`);
			let dataObj;

			if (storedData) {
				dataObj = JSON.parse(storedData);
			} else {
				// Se non esiste ancora, creiamo un nuovo oggetto
				dataObj = {
					nextVoteTime: 0,
					answers: [],
				};
			}

			// Aggiorna la scadenza per rivotare
			dataObj.nextVoteTime = Date.now() + VOTING_COOLDOWN_MINUTES * 60 * 1000;

			// Aggiungi la nuova risposta all’array di risposte
			dataObj.answers.push({
				text: answerText,
				time: Date.now(), // Salviamo anche l'istante in cui è stata data questa risposta
			});

			// Salva nuovamente in localStorage
			localStorage.setItem(`hasVoted_${sessionId}`, JSON.stringify(dataObj));

			//localStorage.setItem(`hasVoted_${sessionId}`, "true");
		},
		/**
		 * Restituisce tutte le risposte salvate per la sessione specificata.
		 * Se non ce ne sono, restituisce un array vuoto.
		 */
		getAllAnswers(sessionId) {
			const storedData = localStorage.getItem(`hasVoted_${sessionId}`);
			if (!storedData) {
				return [];
			}

			const dataObj = JSON.parse(storedData);
			return dataObj.answers || [];
		},

		/**
		 * Facoltativo: fornisce il tempo (in millisecondi) entro cui l’utente potrà rivotare.
		 * Se l'utente può già votare, restituisce 0.
		 */
		getTimeUntilNextVote(sessionId) {
			const storedData = localStorage.getItem(`hasVoted_${sessionId}`);
			if (!storedData) return 0;

			const { nextVoteTime } = JSON.parse(storedData);
			const remaining = nextVoteTime - Date.now();
			return remaining > 0 ? remaining : 0;
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
				this.markVoted(questionId, answerText);
			} catch (error) {
				console.error("Errore:", error);
			}
		},

		async loadVotes() {
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
		async clearVotes(questionId) {
			try {
				const response = await fetch(`${config.API_BASE_URL}/votes/clear/${questionId}`, {
					method: "POST",
				});
				if (!response.ok) {
					throw new Error("Errore nella cancellazaione dei voti");
				}
				await this.loadVotes();
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
