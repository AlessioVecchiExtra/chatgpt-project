import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

export default defineConfig({
  plugins: [vue()],
  resolve: {
    root: '/public',
    alias: {
      '@': '/src'
    }
  }
})
