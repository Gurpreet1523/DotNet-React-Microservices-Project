export default function Footer() {
  return (
    <footer className="footer">
      <div className="container footer-inner">
        <p className="footer-text">
          Built on a .NET microservices backend — Gateway, Auth, Profile, Projects, Skills,
          Contact.
        </p>
        <p className="footer-text">&copy; {new Date().getFullYear()}</p>
      </div>
    </footer>
  );
}
