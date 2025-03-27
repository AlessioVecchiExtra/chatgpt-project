const config = {
	API_BASE_URL: import.meta.env.VITE_API_BASE_URL || "https://api.meeting-with-survey.dev.extra.it/api", // "http://localhost:5184/api",
	CURRENT_MEETING_ID: import.meta.env.VITE_CURRENT_MEETING_ID || 1,
	//API_BASE_URL: "https://localhost:5001/api",
};
console.log("import.meta.env.API_BASE_URL", import.meta.env.VITE_API_BASE_URL);
export default config;
