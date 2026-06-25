import React from 'react';
import Navbar from '../components/Navbar';
import Hero from '../components/Hero';
import SummaryCard from '../components/SummaryCard';

import { useState } from 'react';

const Home = () => {

    const [title, setTitle] = useState("");
    //1. veriyi burada tutacağiz(başlangıçta boş dizi)
    const [summaryPoints, setSummaryPoints] = useState([]);
    
    //2.Backendi bağlarken kullanacağımız fonksiyon
    const handleSummarize = async (link) => {
        // 1 Burada API isteği yapılacak
        try {
            const response = await fetch("http://localhost:5149/api/Summary", {
                method: "POST",
                headers: {
                   "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    youtubeUrl: link
                }),
            
            });
            
            const data = await response.json();
            console.log("BACKEND RESPONSE:", data);
            
            //2 localstorage e kaydetme durumu
            const newItem = {
            title: data.title,
            content: data.content
            };

            // eski kayıtları al
            const oldHistory = JSON.parse(localStorage.getItem("history")) || [];
            
            //dublicate olmaz ard arda aynı card eklenmez
            const exists = oldHistory.find(x => x.title === data.title);

            if (!exists) {
             const updatedHistory = [newItem, ...oldHistory];
            localStorage.setItem("history", JSON.stringify(updatedHistory));
            }
            // yeni kaydı ekle
            const updatedHistory = [newItem, ...oldHistory];

            // tekrar kaydet
            localStorage.setItem("history", JSON.stringify(updatedHistory));
            
            setTitle(data.title);//UI İÇİN BURDA

            // API'den gelen content'i parçala
            const points = data.content
               .split("\n")
               .map(x => x.trim())
               .filter(x => x !== "")
               .filter(x => !x.includes("Genel Özet"));
            
            // Gelen veriyi setSummaryPoints ile state'e atacağız
            setSummaryPoints(points);
        } catch (error) {
            console.error("Hata:", error);
        }
    };
        


    return (
        <div className="home-container">
            
            <Hero onSummarize={handleSummarize} /> {/* Fonksiyonu Hero'ya gönderdik */}
            <h2 style={{ textAlign: "center" }}>{title}</h2>
            <SummaryCard summaryPoints={summaryPoints} title={title} /> {/* Veriyi prop olarak gönderiyoruz */}
             <p className="note">🖋️ Empower your learning. Save hours. Get to the point.</p>
        </div>
    );
}

export default Home