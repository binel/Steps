import React from 'react';
import './StepProgress.css';

function StepProgress({ steps, goal, goalLabel }) {
  const maxSteps = Math.max(steps, goal);
  const overGoal = steps > goal;
  const progressPercent = (steps / maxSteps) * 100;

  return (
    <div className="progress-container">
      <div className="progress-bar-bg">
        <div
          className={`progress-bar-fill ${overGoal ? 'over-goal' : ''}`}
          style={{ width: `${progressPercent}%` }}
        />
      </div>
      <div className="steps-label">{goalLabel} Average: {steps.toLocaleString()} Steps</div>
      <div className="steps-label">Goal: {goal.toLocaleString()} Steps</div>
    </div>
  );
}

export default StepProgress;