import httpClient from './httpClient';

// Each downstream service exposes /health, routed through the gateway.
// This powers the status strip on the home page (see ServiceTopology.jsx).
export const SERVICE_NODES = [
  { key: 'gateway', label: 'Gateway', port: 5000, path: '/health' },
  { key: 'auth', label: 'Auth', port: 5001, path: '/api/auth/health' },
  { key: 'profile', label: 'Profile', port: 5002, path: '/api/profile/health' },
  { key: 'projects', label: 'Projects', port: 5003, path: '/api/projects/health' },
  { key: 'skills', label: 'Skills', port: 5004, path: '/api/skills/health' },
  { key: 'contact', label: 'Contact', port: 5005, path: '/api/contact/health' },
  { key: 'experience', label: 'Experience', port: 5006, path: '/api/experience/health' },
];

const healthService = {
  pingAll: async () => {
    const results = await Promise.all(
      SERVICE_NODES.map(async (node) => {
        try {
          await httpClient.get(node.path, { timeout: 3000 });
          return { ...node, status: 'online' };
        } catch {
          return { ...node, status: 'offline' };
        }
      })
    );
    return results;
  },
};

export default healthService;
