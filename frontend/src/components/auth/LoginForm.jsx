import { useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';

export default function LoginForm() {
  const { login } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();
  const [form, setForm] = useState({ email: '', password: '' });
  const [submitting, setSubmitting] = useState(false);
  const [errorMsg, setErrorMsg] = useState('');

  const from = location.state?.from?.pathname || '/admin';

  const handleChange = (e) => setForm((prev) => ({ ...prev, [e.target.name]: e.target.value }));

  const handleSubmit = async (e) => {
    e.preventDefault();
    setSubmitting(true);
    setErrorMsg('');
    try {
      await login(form);
      navigate(from, { replace: true });
    } catch (err) {
      setErrorMsg(err.message || 'Invalid email or password.');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <form className="contact-form" onSubmit={handleSubmit}>
      <div className="form-field">
        <label htmlFor="email">Email</label>
        <input id="email" name="email" type="email" required value={form.email} onChange={handleChange} />
      </div>
      <div className="form-field">
        <label htmlFor="password">Password</label>
        <input
          id="password"
          name="password"
          type="password"
          required
          value={form.password}
          onChange={handleChange}
        />
      </div>
      {errorMsg && <p className="form-error">{errorMsg}</p>}
      <button className="btn btn-primary" type="submit" disabled={submitting}>
        {submitting ? 'Signing in…' : 'Sign in'}
      </button>
    </form>
  );
}
