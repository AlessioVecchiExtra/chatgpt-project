const config = {
	API_BASE_URL: import.meta.env.VITE_API_BASE_URL,
	CURRENT_MEETING_ID: import.meta.env.VITE_CURRENT_MEETING_ID || 1,	
};
//console.log(import.meta.env.VITE_API_BASE_URL);
export default config;
