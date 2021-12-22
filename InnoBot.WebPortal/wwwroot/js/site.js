let hubConnection;

$(document).ready(function () {

    hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:44348/chat")
        .withAutomaticReconnect()
        .build();
    hubConnection.on("SendQuestion", PrintNewQuestion);
    hubConnection.on("SendFeedback", PrintNewFeedback);

    hubConnection.start();

})

const dateFormatOptions = {
    year: 'numeric',
    month: 'long',
    day: 'numeric'
};
const timeFormatOptions = {
    hour: '2-digit',
    minute: '2-digit'
};

const QuestionTemplate = ({ name, text, date, time }) => `
<div class="media message">
    <img src="~/content/person.png" class="mr-3" alt="...">
    <div class="media-body">
        <h5 class="mt-0">${name}</h5>
        ${text}<br />
        <span class="message-date">${date} ${time}</span>
    </div>
</div>
    `;

const FeedbackTemplate = ({ name, text, proposal, date, time }) => `
<div class="media message">
    <img src="~/content/person.png" class="mr-3" alt="...">
        <div class="media-body text-justify">
            <h5 class="mt-0">${name}</h5>
            ${text}<br />
            <h6 class="mt-1">Future Proposal:</h6>
            ${proposal}<br />
            <span class="message-date float-right">${date} ${time}</span>
        </div>
</div>
    `;
function PrintNewQuestion(question) {
    $('#questions-container').prepend([
        {
            name: question.authorName,
            text: question.text,
            date: new Date(question.asked).toLocaleDateString("en-US", dateFormatOptions),
            time: new Date(question.asked).toLocaleTimeString("en-US", timeFormatOptions)
        }
    ].map(QuestionTemplate).join(''));
}

function PrintNewFeedback(feedback) {
    $('#feedbacks-container').prepend([
        {
            name: feedback.authorName,
            text: feedback.questionText,
            proposal: feedback.futureProposal,
            date: new Date(feedback.sent).toLocaleDateString("en-US", dateFormatOptions),
            time: new Date(feedback.sent).toLocaleTimeString("en-US", timeFormatOptions)
        }
    ].map(FeedbackTemplate).join(''));
}