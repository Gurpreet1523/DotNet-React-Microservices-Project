import httpClient from './httpClient';

// Routed by Ocelot: /api/profile/** -> Portfolio.Profile.API (:5002)
const profileService = {
  getProfile: async () => {
    const { data } = await httpClient.get('/api/profile');
    return data;
  },

  updateProfile: async (payload) => {
    const { data } = await httpClient.put('/api/profile', payload);
    return data;
  },

  getExperience: async () => {
    const { data } = await httpClient.get('/api/profile/experience');
    return data;
  },

  getEducation: async () => {
    const { data } = await httpClient.get('/api/profile/education');
    return data;
  },
};

export default profileService;
