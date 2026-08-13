import httpClient from './httpClient';

// Routed by Ocelot: /contact/** -> Portfolio.Contact.API (:5005)
const contactService = {
  sendMessage: async (payload) => {
    const { data } = await httpClient.post('/contact', payload);
    return data;
  },

  // Admin-only: requires JWT with the right role/claim.
  getMessages: async () => {
    const { data } = await httpClient.get('/contact');
    return data;
  },
};

export default contactService;
