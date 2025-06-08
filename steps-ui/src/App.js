import React, { useState, useEffect } from 'react';
import './App.css';
import StepProgress from './StepProgress';
import Stats from './Stats';
import StepsTable from './StepsTable';

const apiUrl = process.env.REACT_APP_API_URL;

const filterStepsAfterDate = (data, cutoffDate) => {
  const cutoff = new Date(cutoffDate);
  return data.filter(entry => new Date(entry.date) >= cutoff);
};

function getWeekData(data) {
  const today = new Date();
  const oneWeekAgo = new Date();
  oneWeekAgo.setDate(today.getDate() - 7);
  return filterStepsAfterDate(data, oneWeekAgo);
}

function getMonthData(data) {
  const today = new Date();
  const oneMonthAgo = new Date();
  oneMonthAgo.setDate(today.getDate() - 30);
  return filterStepsAfterDate(data, oneMonthAgo);  
}

function computeAverageSteps(data) {
  if (data.length === 0) return 0;
  const total = data.reduce((sum, entry) => sum + entry.steps, 0);
  return Math.round(total / data.length);
}

function computeTotalSteps(data) {
  if (data.length === 0) return 0;
  const total = data.reduce((sum, entry) => sum + entry.steps, 0);
  return total;
}

function App() {
  const loadSteps = () => {
    console.log(`trying to call with url ${apiUrl}`);
    fetch(`${apiUrl}/steps/getStepEntries`)
        .then((res) => {
          if (!res.ok) {
            throw new Error(`Server responded with status ${res.status}`);
          }
          return res.json();
        })
        .then(setStepsData)
        .catch((err) => {
          console.error('Failed to fetch steps:', err);
        });
  }

  const onAdd = (addData) => {
    const requestBody = {
        Steps: addData.steps,
        Date: addData.date
    };

    fetch(`${apiUrl}/steps/addEntry`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(requestBody)
    })
        .then((res) => {
            if (!res.ok) throw new Error('Failed to submit new steps');
            return res.text;
        })
        .then(() => {
            loadSteps();
        })
        .catch((err) => {
            console.error(err);
        })
  }  

  const onDelete = (deleteId) => {
    fetch(`${apiUrl}/steps/deleteEntry?id=${deleteId}`, {
      method: 'DELETE',
      headers: {
      },
  })
      .then((res) => {
          if (!res.ok) throw new Error('Failed to delete');
          return res.text;
      })
      .then(() => {
          loadSteps();
      })
      .catch((err) => {
          console.error(err);
      })
  }
  
  useEffect(() => {
    loadSteps();
  }, []);

  const [stepsData, setStepsData] = useState([]);
  
  const days = stepsData.length;
  const weekData = getWeekData(stepsData);
  const weekAverageSteps = computeAverageSteps(weekData);
  const monthData = getMonthData(stepsData);
  const monthAverageSteps = computeAverageSteps(monthData);
  const totalSteps = computeTotalSteps(stepsData);

  return (
    <div className="App">
      <h1>Steps</h1>
      <div>
        <StepProgress steps={weekAverageSteps} goal={15000} goalLabel={"One Week"}></StepProgress>
      </div>
      <div>
        <StepProgress steps={monthAverageSteps} goal={15000} goalLabel={"One Month"}></StepProgress>
      </div>
      <div>
        <Stats totalSteps={totalSteps} days={days}></Stats>
      </div>
      <div>
        <StepsTable data={stepsData} onAdd={onAdd} onDelete={onDelete}/>
      </div>
    </div>
  );
}

export default App;
