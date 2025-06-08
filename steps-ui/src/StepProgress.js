import React from 'react';
import PropTypes from 'prop-types';
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

StepProgress.propTypes = {
  steps: PropTypes.number.isRequired,
  goal: PropTypes.number.isRequired,
  goalLabel: PropTypes.string.isRequired,
};

export default StepProgress;