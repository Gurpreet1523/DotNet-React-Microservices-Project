import ProjectGrid from '../components/projects/ProjectGrid';

export default function Projects() {
  return (
    <section className="section">
      <div className="container">
        <p className="eyebrow">Selected work</p>
        <h2>Projects</h2>
        <ProjectGrid />
      </div>
    </section>
  );
}
