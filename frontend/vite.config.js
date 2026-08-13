import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

// All API traffic goes through the Ocelot Gateway on :5000.
// The gateway then routes to Auth/Profile/Projects/Skills/Contact services.
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    proxy: {
      '/api': {
        target: 'http://localhost:5000',
        changeOrigin: true,
        secure: false,
      },
    },
  },
});
