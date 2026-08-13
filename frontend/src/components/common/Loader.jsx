export default function Loader({ label = 'Loading' }) {
  return (
    <div className="loader" role="status" aria-live="polite">
      <span className="loader-dot" />
      <span className="loader-label">{label}…</span>
    </div>
  );
}
