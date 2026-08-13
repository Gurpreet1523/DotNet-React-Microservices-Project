import { useState } from 'react';
import contactService from '../../api/contactService';

const initialForm = { name: '', email: '', message: '' };

export default function ContactForm() {
  const [form, setForm] = useState(initialForm);
  const [status, setStatus] = useState('idle'); // idle | submitting | success | error
  const [errorMsg, setErrorMsg] = useState('');

  const handleChange = (e) => {
    setForm((prev) => ({ ...prev, [e.target.name]: e.target.value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setStatus('submitting');
    setErrorMsg('');
    try {
      await contactService.sendMessage(form);
      setStatus('success');
      setForm(initialForm);
    } catch (err) {
      setStatus('error');
      setErrorMsg(err.message || 'Message could not be sent. Try again.');
    }
  };

  if (status === 'success') {
    return (
      <div className="contact-success card">
        <p>Message sent. I'll get back to you soon.</p>
        <button className="btn" onClick={() => setStatus('idle')}>
          Send another
        </button>
      </div>
    );
  }

  return (
    <form className="contact-form" onSubmit={handleSubmit}>
      <div className="form-field">
        <label htmlFor="name">Name</label>
        <input
          id="name"
          name="name"
          type="text"
          required
          value={form.name}
          onChange={handleChange}
        />
      </div>

      <div className="form-field">
        <label htmlFor="email">Email</label>
        <input
          id="email"
          name="email"
          type="email"
          required
          value={form.email}
          onChange={handleChange}
        />
      </div>

      <div className="form-field">
        <label htmlFor="message">Message</label>
        <textarea
          id="message"
          name="message"
          rows={6}
          required
          value={form.message}
          onChange={handleChange}
        />
      </div>

      {status === 'error' && <p className="form-error">{errorMsg}</p>}

      <button className="btn btn-primary" type="submit" disabled={status === 'submitting'}>
        {status === 'submitting' ? 'Sending…' : 'Send message'}
      </button>
    </form>
  );
}
