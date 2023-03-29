import React from 'react';
import { Line } from 'react-chartjs-2'
import {
    Chart as ChartJS,
    BarElement,
    CategoryScale,
    LinearScale,
    Tooltip,
    PointElement,
    LineElement,
    LineController,
    Legend
} from 'chart.js';

ChartJS.register(
    BarElement,
    CategoryScale, // x axis weeks
    LinearScale, // y axis number of requests
    Tooltip,
    Legend,
    PointElement,
    LineElement,
    LineController
    )

const TestimonialGraph = ({ testimonial }) => {

    // requests: [15, 1, 11, 3, 7, 6, 0, 8, 44]
    // weeks: //[31, 33, 37, 41, 45, 50, 1, 5, 9]
    // result: [0,0,0,0,...15,0,1,0,0,0,0,11....]

    console.log("TESTIMONIAL",testimonial);

    const weekData = Array.from({ length: 52 }, (_, i) => {
       // const index = i + 1;
        if (testimonial[i]) { // if value exists
            return {
                RequestsSent: testimonial[i].RequestsSent || 0,
                RequestsCompleted: testimonial[i].RequestsCompleted || 0,
                Year: testimonial[i].Year || 0,
                Week: testimonial[i].Week || 0
            }; // return those elememts in array
        } else {
            return { RequestsSent: 0, RequestsCompleted: 0, Week: i }; // else return 0
        }
    });

    console.log("WEEKDATA", weekData);

      const weeksCompleted = weekData.map((data) => data.Week); // [31, 33, 37, 41, 45, 50, 1, 5, 9]
      console.log("WEEKS", weeksCompleted); 

    /*const week = Array.from({ length: 52 }, (_, i) => {
        if (testimonial[i]) {
            return { Week: testimonial[i].Week };
        } else {
            return { Week: i };
        }
    });*/

    //console.log("Data", weekData);

  //  console.log("Weeks:", week);

       // const requestsCompleted = weekData.map((data) => data.RequestsCompleted);
   // console.log(requestsCompleted);  // [15,1,11,3,7,6,0,8,44]

   // const weeksCompleted = weekData.map((data) => data.Week); // [31, 33, 37, 41, 45, 50, 1, 5, 9]
   // console.log("WEEKS", weeksCompleted); 

    const week = testimonial.map(item => ({ Year: item.Year, Week: item.Week }));
    const labels = week.map((week, index) => `Week ${week.Week}, ${week.Year}`);


    const data = {
        labels,
        datasets: [
            {
                label: 'Requests Sent',
                data: weekData.map((week) => week.RequestsSent),
                backgroundColor: 'aqua',
                borderColor: 'black',
                borderWidth: 1,
            },
                {
                label: 'Requests Completed',
                data: weekData.map((week) => week.RequestsCompleted),
                backgroundColor: 'blue',
                borderColor: 'black',
                borderWidth: 1,
            }
        ]
    }

    const options = {
        responsive: true,
        maintainAspectRatio: false,
        title: {
            display: true,
            text: 'My Chart Title'
        },
        scales: {
            y: {
                suggestedMin: 0,
                suggestedMax: testimonial.length
                }
        },

    }

    return (
        <div>
            <Line
                data={data}
                options={options}
                style={{ height: "400px", width: "600px" }} 

            >
            </Line>
            </div>
      )
}

export default TestimonialGraph