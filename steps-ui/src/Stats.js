import React from 'react';
import PropTypes from 'prop-types';
import './Stats.css';

function Stats({ totalSteps, days }) {

    if (typeof totalSteps !== 'number') {
        console.warn(`Invalid prop 'totalSteps': expected number, got ${typeof steps}`);
        totalSteps = 0;
      }
    
      if (typeof days !== 'number') {
        console.warn(`Invalid prop 'days': expected number, got ${typeof goal}`);
        days = 0;
      }   

    const additionalSteps = totalSteps - (5000 * days)
    const calories = (additionalSteps / 1000) * 50;
    const fatPounds = calories / 3500;
    const mcChickens = Math.round(calories / 390);
    const pizza = Math.round(calories / 1600);
    const mandm = Math.round(calories / 4);
    const miles = (totalSteps / 2000);
    const usWidth = (miles / 2800) * 100;
    return (
      <div className="stats-container">
        <div className="stats-label">
            You&apos;ve taken <b>{totalSteps.toLocaleString()}</b> steps since the project began <b>{days.toLocaleString()}</b> days ago.
        </div>
        <div className="stats-label">
            Compared against your previous average of <b>5000</b> steps per day, that&apos;s an additional <b>{additionalSteps.toLocaleString()}</b> steps.
        </div>        
        <div className="stats-label">
            That&apos;s around <b>{calories.toLocaleString()}</b> calories. 
        </div>
        <div className="stats-label">
            Which is around <b>{fatPounds.toLocaleString()}</b> pounds of fat. 
        </div>
        <div className="stats-label">
            That&apos;s about <b>{mcChickens.toLocaleString()}</b> McChickens,
        </div>
        <div className="stats-label">
            <b>{pizza.toLocaleString()}</b> medium Dominos Pepperoni Pizzas, 
        </div>
        <div className="stats-label">
            or <b>{mandm.toLocaleString()}</b> M&Ms. 
        </div>
        <div className="stats-label">
            In total you&apos;ve traveled about <b>{miles.toLocaleString()}</b> miles. 
        </div>
        <div className="stats-label">
            Which is about about <b>{usWidth.toLocaleString()}%</b> of the width of the United States. 
        </div>
      </div>
    );
  }

  Stats.propTypes = {
    totalSteps: PropTypes.number.isRequired,
    days: PropTypes.number.isRequired,
  };
  
  export default Stats;