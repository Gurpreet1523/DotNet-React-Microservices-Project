import { useFetch } from '../../hooks/useFetch';
import profileService from '../../api/profileService';
import Loader from '../common/Loader';
import ErrorMessage from '../common/ErrorMessage';
import ServiceTopology from './ServiceTopology';

export default function Hero() {
  const { data: profile, loading, error, refetch } = useFetch(profileService.getProfile, []);

  return (
    <section className="hero">
      <div className="container">
        <p className="eyebrow">full-stack Software Developer</p>

        {loading && <Loader label="Loading profile" />}
        {error && <ErrorMessage message="Couldn't reach the Profile service." onRetry={refetch} />}

        {profile && (
          <>
            <h1 className="hero-title">{profile.fullName}</h1>
            <p className="hero-summary">{profile.summary}</p>
            <div className="hero-actions">
              <a className="btn btn-primary" href="/projects">
                View projects
              </a>
              <a className="btn" href={profile.resumeUrl || '#'} download>
                Download resume
              </a>
            </div>
          </>
        )}

        <div className="hero-topology">
          <ServiceTopology />
        </div>
      </div>
    </section>
  );
}
