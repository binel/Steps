import React, { useState } from 'react';
import './StepsTable.css';

function StepsTable({ data, onDelete }) {
  const [pendingDelete, setPendingDelete] = useState(null);

  const confirmDelete = () => {
    onDelete(pendingDelete);
    setPendingDelete(null);
  };

  const cancelDelete = () => setPendingDelete(null);

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
          {data.map((entry) => (
            <tr key={entry.date}>
              <td>{entry.date}</td>
              <td>{entry.steps.toLocaleString()}</td>
              <td>
                <button className="delete-btn" onClick={() => setPendingDelete(entry.date)}>
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
            <p>Are you sure you want to delete the entry for <strong>{pendingDelete}</strong>?</p>
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

export default StepsTable;