import { Link } from 'react-router-dom';

export default function NotFound() {
  return (
    <section className="section">
      <div className="container not-found">
        <p className="eyebrow">404</p>
        <h2>This route doesn't exist.</h2>
        <p className="contact-intro">
          The gateway routed you here, but there's no page behind it.
        </p>
        <Link to="/" className="btn btn-primary">
          Back home
        </Link>
      </div>
    </section>
  );
}
