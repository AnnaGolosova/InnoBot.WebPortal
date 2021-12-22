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

const QuestionTemplate = ({ name, text, date }) => `
<div class="media message">
    <img src="/content/person.png" class="mr-3" alt="...">
    <div class="media-body">
        <h5 class="mt-0">${name}</h5>
        ${text}<br />
        <span class="message-date float-right">${date}</span>
    </div>
</div>
    `;

const FeedbackTemplate = ({ name, text, proposal, date }) => `
<div class="media message">
    <img src="/content/person.png" class="mr-3" alt="...">
        <div class="media-body text-justify">
            <h5 class="mt-0">${name}</h5>
            ${text}<br />
            <h6 class="mt-1">Future Proposal:</h6>
            ${proposal}<br />
            <span class="message-date float-right">${date}</span>
        </div>
</div>
    `;
function PrintNewQuestion(question) {
    let askedDate = new Date(question.asked);

    $('#questions-container').prepend([
        {
            name: question.authorName,
            text: question.questionText,
            date: askedDate.getHours() + ':' + askedDate.getMinutes() + ' ' + askedDate.getFullYear() + '/' + askedDate.getMonth() + '/' + askedDate.getDate()
        }
    ].map(QuestionTemplate).join(''));
}

function PrintNewFeedback(feedback) {
    let sentDate = new Date(feedback.sent);

    $('#feedbacks-container').prepend([
        {
            name: feedback.authorName,
            text: feedback.message,
            proposal: feedback.futureProposal,
            date: sentDate.getHours() + ':' + sentDate.getMinutes() + ' ' + sentDate.getFullYear() + '/' + sentDate.getMonth() + '/' + sentDate.getDate()
        }
    ].map(FeedbackTemplate).join(''));
}