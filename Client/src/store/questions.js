import { defineStore } from "pinia";
import config from "@/config.js";

export const useQuestionsStore = defineStore("questions", {
	state: () => ({
		// Mappa sessionId -> oggetto { questionText, terms: [] }
		questionsMap: {},
		questionsCount: 0,
	}),
	actions: {
		async loadQuestion(sessionId) {
			try {
				// Esempio: GET /api/Questions/1
				// const response = await fetch(
				// 	`${config.API_BASE_URL}/questions/${config.CURRENT_MEETING_ID}/getById/${sessionId}`
				// );
				// if (!response.ok) {
				// 	throw new Error("Errore nel recupero della domanda");
				// }
				//const data = await response.json();
				
				// data ha la forma:
				// {
				//   "sessionId": 1,
				//   "questionText": "Quale parola rappresenta meglio la Sessione 1?",
				//   "questionAnswers": [ ...10 termini... ]
				// }

				const responseAll = await fetch(`${config.API_BASE_URL}/questions/${config.CURRENT_MEETING_ID}`);
				if (!responseAll.ok) {
					throw new Error("Errore nel recupero della domanda");
				}
				const dataAll = await responseAll.json();
				
				this.questionsCount = dataAll.length;
				const data = dataAll.find((x) => x.id == sessionId);

				console.log(config.CURRENT_MEETING_ID)
				// Salviamo i dati nello state
				this.questionsMap[sessionId] = {
					questionText: data.questionText,
					questionAnswers: data.questionAnswers,
				};
			} catch (error) {
				console.error("Errore:", error);
			}
		},
	},
	getters: {
		nextSessionId: (state) => (sessionId) => {
			let currentId = parseInt(sessionId) + 1;
			if (isNaN(currentId) || currentId > state.questionsCount) {
				currentId = 1;
			}
			return currentId;
		},
	},
});
