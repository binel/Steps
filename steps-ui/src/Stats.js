import React from 'react';
import './Stats.css';

function Stats({ totalSteps }) {
    const calories = (totalSteps / 1000) * 50;
    const fatPounds = calories / 3500;
    const mcChickens = Math.round(calories / 390);
    const pizza = Math.round(calories / 1600);
    const miles = (totalSteps / 2000);
    const usWidth = (miles / 2800) * 100;
    return (
      <div className="stats-container">
        <div className="stats-label">
            You've taken <b>{totalSteps.toLocaleString()}</b> steps since the project began.
        </div>
        <div className="stats-label">
            That's around <b>{calories.toLocaleString()}</b> calories. 
        </div>
        <div className="stats-label">
            Which is around <b>{fatPounds.toLocaleString()}</b> pounds of fat. 
        </div>
        <div className="stats-label">
            Or around <b>{mcChickens.toLocaleString()}</b> McChickens. 
        </div>
        <div className="stats-label">
            Or around <b>{pizza.toLocaleString()}</b> medium Dominos Pepperoni Pizzas. 
        </div>
        <div className="stats-label">
            You've traveled about <b>{miles.toLocaleString()}</b> miles. 
        </div>
        <div className="stats-label">
            Which is about about <b>{usWidth.toLocaleString()}%</b> of the width of the United States. 
        </div>
      </div>
    );
  }
  
  export default Stats;