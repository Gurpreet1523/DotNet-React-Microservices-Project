import httpClient from './httpClient';

// Routed by Ocelot: /experience/** -> Portfolio.Experience.API (:5006)
const experienceService = {
   getExperience: async () => {
    const { data } = await httpClient.get('/experience');
    return data;
  },

  updateExperience: async (payload) => {
    const { data } = await httpClient.put('/experience', payload);
    return data;
  },

  getEducation: async () => {
    const { data } = await httpClient.get('/profile/education');
    return data;
  },
};

export default experienceService;
