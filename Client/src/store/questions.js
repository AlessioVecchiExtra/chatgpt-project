import { defineStore } from "pinia";
import config from "@/config.js";

export const useQuestionsStore = defineStore("questions", {
	state: () => ({
		// Mappa sessionId -> oggetto { questionText, terms: [] }
		//questionsMap: { [config.CURRENT_MEETING_ID]: {} },
		questionsCount: 0,
		questions: [],
	}),
	actions: {
		async loadQuestions() {
			const response = await fetch(`${config.API_BASE_URL}/questions/${config.CURRENT_MEETING_ID}`);
			if (!response.ok) {
				throw new Error("Errore nel recupero delle domande");
			}
			this.questions = await response.json();
			this.questionsCount = this.questions.length;
			return this.questions;
		},
		// async loadQuestion(sessionId) {
		// 	try {
		// 		const items = await this.loadQuestions();
		// 		const data = items.find((x) => x.id == sessionId);

		// 		// Salviamo i dati nello state
		// 		this.questionsMap[config.CURRENT_MEETING_ID] = {};
		// 		this.questionsMap[config.CURRENT_MEETING_ID][sessionId] = {
		// 			questionText: data.questionText,
		// 			questionAnswers: data.questionAnswers,
		// 		};
		// 	} catch (error) {
		// 		console.error("Errore:", error);
		// 	}
		// },
	},
	getters: {
		getQuestionById: (state) => (sessionId) => {
			return state.questions.find((x) => x.id == sessionId);
		},
		nextSessionId: (state) => (sessionId) => {
			if (state.questionsCount == 0) return;
			const current = state.questions.find((x) => x.id == sessionId);
			const currentIndex = state.questions.indexOf(current);
			let nextIndex = 0;
			if (currentIndex < state.questionsCount - 1) {
				nextIndex = currentIndex + 1;
			}
			//console.log(nextIndex);
			return state.questions[nextIndex].id;

			// let currentId = parseInt(sessionId) + 1;
			// if (isNaN(currentId) || currentId > state.questionsCount) {
			// 	currentId = 1;
			// }
			// return currentId;
		},
	},
});
