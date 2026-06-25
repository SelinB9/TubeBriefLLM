import React from 'react'
import './Hero.css';
import { FaMagic, FaLink} from "react-icons/fa";
import { useState} from 'react';

const Hero = ({ onSummarize }) => {

  const [inputValue, setInputValue] = useState("");

  return (
    <div className='hero'>
      <div className="ai-badge">
                <FaMagic /> AI-Powered Video Summarizer
            </div>
      <h1>Summarize YouTube Videos<br /><span className='highlight1'>in Seconds</span>  <span className="highlight">Not Minutes</span></h1>
      <p className="subtitle">Get concise, accurate summaries of YouTube videos.<br /> Save time. Learn more. Stay ahead.</p>
      <div className='input-container'>
        <input type='text' placeholder='🔗 Paste YouTube Link...'
          value={inputValue}
          onChange={(e) => setInputValue(e.target.value)}
        />
       
        <button onClick={()=> onSummarize(inputValue)}>
          <FaMagic />Summarize
        </button>
      </div>
      <p className="privacy-note">🔒 We don't store your links or data. Your privacy is safe with us.</p>
    </div>
  );
}

export default Hero