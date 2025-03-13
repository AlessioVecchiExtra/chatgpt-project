const config = {
  API_BASE_URL: import.meta.env.VITE_API_BASE_URL || "http://localhost:5184/api",
  CURRENT_MEETING_ID:  import.meta.env.CURRENT_MEETING_ID ||  1,
  //API_BASE_URL: "https://localhost:5001/api",
};
export default config;
