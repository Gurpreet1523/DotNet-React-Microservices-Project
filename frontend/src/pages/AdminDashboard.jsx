import { useFetch } from '../hooks/useFetch';
import contactService from '../api/contactService';
import Loader from '../components/common/Loader';
import ErrorMessage from '../components/common/ErrorMessage';

export default function AdminDashboard() {
  const { data: messages, loading, error, refetch } = useFetch(contactService.getMessages, []);

  return (
    <section className="section">
      <div className="container">
        <p className="eyebrow">Admin</p>
        <h2>Inbox</h2>

        {loading && <Loader label="Loading messages" />}
        {error && <ErrorMessage message="Couldn't reach the Contact service." onRetry={refetch} />}
        {messages && messages.length === 0 && <p className="empty-state">No messages yet.</p>}

        {messages && messages.length > 0 && (
          <ul className="inbox-list">
            {messages.map((msg) => (
              <li key={msg.id} className="card inbox-item">
                <div className="inbox-item-head">
                  <strong>{msg.name}</strong>
                  <span className="inbox-item-email">{msg.email}</span>
                </div>
                <p>{msg.message}</p>
              </li>
            ))}
          </ul>
        )}
      </div>
    </section>
  );
}
