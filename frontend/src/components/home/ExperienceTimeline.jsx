import { useFetch } from '../../hooks/useFetch';
import profileService from '../../api/profileService';
import Loader from '../common/Loader';
import ErrorMessage from '../common/ErrorMessage';

export default function ExperienceTimeline() {
  const { data: items, loading, error, refetch } = useFetch(profileService.getExperience, []);

  return (
    <section className="section">
      <div className="container">
        <p className="eyebrow">Experience</p>
        <h2>Where I've worked</h2>

        {loading && <Loader label="Loading experience" />}
        {error && <ErrorMessage message="Couldn't reach the Profile service." onRetry={refetch} />}

        {items && (
          <ol className="timeline">
            {items.map((item) => (
              <li className="timeline-item" key={item.id}>
                <div className="timeline-marker" />
                <div className="timeline-content">
                  <div className="timeline-heading">
                    <h3>{item.role}</h3>
                    <span className="timeline-range">
                      {formatDate(item.startDate)} — {item.endDate ? formatDate(item.endDate) : 'Present'}
                    </span>
                  </div>
                  <p className="timeline-company">{item.company}</p>
                  {item.highlights?.length > 0 && (
                    <ul className="timeline-highlights">
                      {item.highlights.map((h, i) => (
                        <li key={i}>{h}</li>
                      ))}
                    </ul>
                  )}
                </div>
              </li>
            ))}
          </ol>
        )}
      </div>
    </section>
  );
}

function formatDate(value) {
  const date = new Date(value);
  return date.toLocaleDateString(undefined, { month: 'short', year: 'numeric' });
}
