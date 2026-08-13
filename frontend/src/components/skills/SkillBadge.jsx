export default function SkillBadge({ skill }) {
  return (
    <div className="skill-badge">
      <span className="skill-badge-name">{skill.name}</span>
      <div className="skill-badge-bar">
        <div className="skill-badge-fill" style={{ width: `${skill.proficiency}%` }} />
      </div>
    </div>
  );
}
