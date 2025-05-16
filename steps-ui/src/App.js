import React, { useState } from 'react';
import './App.css';
import StepProgress from './StepProgress';
import Stats from './Stats';
import StepsTable from './StepsTable';



function App() {
  const [stepsData, setStepsData] = useState([
    { date: '2025-05-15', steps: 7707 },
    { date: '2025-05-14', steps: 10761 },
    { date: '2025-05-13', steps: 11313 },
    { date: '2025-05-12', steps: 10970 },
  ]);
  
  const handleDelete = (dateToDelete) => {
    console.log("Fake deleting " + dateToDelete);
  };

  return (
    <div className="App">
      <h1>Steps</h1>
      <div>
        <StepProgress steps={10120} goal={12000}></StepProgress>
      </div>
      <div>
        <Stats totalSteps={10120 * 4}></Stats>
      </div>
      <div>
        <StepsTable data={stepsData} onDelete={handleDelete} />
      </div>
    </div>
  );
}

export default App;
