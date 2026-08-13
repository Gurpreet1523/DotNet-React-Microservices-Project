import LoginForm from '../components/auth/LoginForm';

export default function Login() {
  return (
    <section className="section">
      <div className="container">
        <p className="eyebrow">Admin</p>
        <h2>Sign in</h2>
        <LoginForm />
      </div>
    </section>
  );
}
