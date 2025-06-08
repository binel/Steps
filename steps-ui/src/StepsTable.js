import React, { useState } from 'react';
import PropTypes from 'prop-types';
import './StepsTable.css';

function StepsTable({ data, onAdd, onDelete }) {
  const [pendingDelete, setPendingDelete] = useState(null);
  const [newDate, setNewDate] = useState('');
  const [newSteps, setNewSteps] = useState('');

  const confirmDelete = () => {
    onDelete(pendingDelete);
    setPendingDelete(null);
  };

  const cancelDelete = () => setPendingDelete(null);

  const handleAdd = () => {
    if (!newDate || !newSteps) return;
    onAdd({ date: newDate, steps: parseInt(newSteps, 10) });
    setNewDate('');
    setNewSteps('');
  };

  return (
    <div className="steps-table-container">
      <table className="steps-table">
        <thead>
          <tr>
            <th>Date</th>
            <th>Steps</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
        <tr>
            <td>
              <input
                type="date"
                value={newDate}
                onChange={(e) => setNewDate(e.target.value)}
              />
            </td>
            <td>
              <input
                type="number"
                value={newSteps}
                onChange={(e) => setNewSteps(e.target.value)}
                placeholder="Steps"
              />
            </td>
            <td>
              <button onClick={handleAdd}>Add</button>
            </td>
          </tr>            
          {data.map((entry) => (
            <tr key={entry.id}>
              <td>{entry.date}</td>
              <td>{entry.steps.toLocaleString()}</td>
              <td>
                <button className="delete-btn" onClick={() => setPendingDelete(entry.id)}>
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {pendingDelete && (
        <div className="dialog-overlay">
          <div className="dialog">
            <p>Are you sure you want to delete this entry?</p>
            <div className="dialog-actions">
              <button onClick={cancelDelete} className="cancel-btn">Cancel</button>
              <button onClick={confirmDelete} className="confirm-btn">Delete</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

StepsTable.propTypes = {
  data: PropTypes.object.isRequired,
  onAdd: PropTypes.func.isRequired,
  onDelete: PropTypes.func.isRequired,
};

export default StepsTable;