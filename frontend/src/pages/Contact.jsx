import ContactForm from '../components/contact/ContactForm';

export default function Contact() {
  return (
    <section className="section">
      <div className="container">
        <p className="eyebrow">Get in touch</p>
        <h2>Contact</h2>
        <p className="contact-intro">
          Send a message and it'll land straight in the Contact service — no third-party form
          tool in the way.
        </p>
        <ContactForm />
      </div>
    </section>
  );
}
