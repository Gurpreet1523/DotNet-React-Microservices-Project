export default function ErrorMessage({ message = 'This section failed to load.', onRetry }) {
  return (
    <div className="error-box" role="alert">
      <p>{message}</p>
      {onRetry && (
        <button className="btn" onClick={onRetry}>
          Retry
        </button>
      )}
    </div>
  );
}
