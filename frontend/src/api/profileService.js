import httpClient from './httpClient';

// Routed by Ocelot: /api/profile/** -> Portfolio.Profile.API (:5002)
const profileService = {
  getProfile: async () => {
    const { data } = await httpClient.get('/profile');
    return data;
  },

  updateProfile: async (payload) => {
    const { data } = await httpClient.put('/profile', payload);
    return data;
  },

  getExperience: async () => {
    const { data } = await httpClient.get('/profile/experience');
    return data;
  },

  getEducation: async () => {
    const { data } = await httpClient.get('/profile/education');
    return data;
  },
};

export default profileService;
