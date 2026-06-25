import React, { useEffect, useState } from 'react';
import SummaryCard from '../components/SummaryCard';
import './History.css';

const History = () => {

    const [history, setHistory] = useState([]);

    useEffect(() => {
        const data = JSON.parse(localStorage.getItem("history") || "[]");
        setHistory(data);
    }, []);

    return (
        
         
            <div className='history-container'>
            <h1 style={{ textAlign: "center" }}>History</h1>
            
            <div className='history-grid'>
            {history.map((item, index) => {
                const points = item.content
                    .split("\n")
                    .filter(x => x.trim() !== "");

                return (
                    <SummaryCard className='summary-cards'
                        key={index}
                         title={item.title}
                        summaryPoints={points}
                    />
                );
            })}
            </div>
             <p className="note">🖋️ Empower your learning. Save hours. Get to the point.</p>
            </div>
           
    );
};

export default History;