import React from 'react'
import './SummaryCard.css';
import { FaCheckCircle } from "react-icons/fa";

const SummaryCard = ({ summaryPoints ,title}) => {
  // Gelen veriyi satırlara bölüp array yapıyoruz
  const pointsArray = Array.isArray(summaryPoints)
    ? summaryPoints
    : summaryPoints?.split('\n').filter(p => p.trim() !== '') || [];

  // "Genel Özet" satırını maddelerden ayırıyoruz
  const mainPoints = pointsArray.filter(p => !p.toLowerCase().includes('genel özet'));
  const generalSummary = pointsArray.find(p => p.toLowerCase().includes('genel özet'));
  
  return (
    <>
      <div className='summary-cards'>
        <div className='card-header'>
          <h3>Top 5 Key Points <span className="video-title">{title}</span> </h3>
          <span className='ai-badge-small'>
            <FaCheckCircle />AI Generated
          </span>
        </div>
       
        <div className='points-list'>
          {mainPoints.map((point, index) => (
            <div key={index} className='point-item'>
              {/* Sayı tam mavi balonun içine yazılıyor */}
              <span className='number'>{index + 1}</span>
              <p className='point-text'>{point}</p>
            </div>
          ))}
        </div>
      
        {generalSummary && (
          <div className='general-summary-section'>
            <h4>📌 Genel Özet</h4>
            <p>{generalSummary}</p>
          </div>)}
      </div>
     
    </>
  );
}

export default SummaryCard;