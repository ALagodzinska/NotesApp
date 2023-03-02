const uri = "api/Note";
const uriToGetUser = "api/User";

// container for all notes
const allNotesConatiner = document.getElementById("notes-index");

let currentUser = {};

const getCurrentUser = () =>
  fetch(uriToGetUser)
    .then((response) => response.json())
    .catch((error) => console.error("Unable to get current user.", error));

// add read more btn to the long text
const addReadMoreBtn = function (currentNote) {
  // note p elemnet
  const noteContentElement = currentNote.querySelector(".card-text");

  // only if text is longer than four lines
  if (
    noteContentElement.offsetHeight < noteContentElement.scrollHeight ||
    noteContentElement.offsetWidth < noteContentElement.scrollWidth
  ) {
    const buttonReadMore = document.createElement("button");
    buttonReadMore.classList.add("btn-readmore");
    buttonReadMore.innerHTML = "Read More";
    // add event on click
    buttonReadMore.addEventListener("click", function () {
      if (noteContentElement.classList.contains("cutoff-text")) {
        noteContentElement.classList.remove("cutoff-text");
        buttonReadMore.innerHTML = "Read Less";
      } else {
        noteContentElement.classList.add("cutoff-text");
        buttonReadMore.innerHTML = "Read More";
      }
    });

    noteContentElement.after(buttonReadMore);
  }
};

const displayNotes = function (data) {
  // clear the notes container
  allNotesConatiner.innerHTML = "";

  // create an html element foreach of them
  data.forEach((note) => {
    // display todo or text based on note type
    const noteContent =
      note.type === 1
        ? `${note.toDoList
            .map((todo) => {
              const isDone = todo.isDone ? "checked" : "";
              const checkbox = `<input type="checkbox" ${isDone} disabled>`;
              return `${checkbox} ${todo.content}<br/>`;
            })
            .join("")}`
        : `${note.textContent}`;

    const noteHtml = `<div class="col-md-4" id="note-${note.id}">
                    <div class="card mb-4 box-shadow ${note.colorClass} one-note-di">
                        <div class="card-body">
                            <h5 class="card-title">${note.title}</h5>
                            <p class="card-text cutoff-text">
                                ${noteContent}
                            </p>
                            <div class="btn-group btn-light btn-outline-light">
                                <a href="/Notes/Edit?id=${note.id}" class="btn btn-sm btn-outline-secondary">Edit</a>
                                <a href="/Notes/Delete?id=${note.id}" class="btn btn-sm btn-outline-secondary">Delete</a>
                                <a href="/Notes/Share?id=${note.id}" class="btn btn-sm btn-outline-secondary share-btn"><span>Share</span></a>
                            </div>                   
                        </div>                        
                    </div>
                </div>`;

    // add note to page
    allNotesConatiner.insertAdjacentHTML("afterbegin", noteHtml);

    // get current note element
    const currentNote = document.getElementById(`note-${note.id}`);

    // add read more btn
    addReadMoreBtn(currentNote);
  });
};

const getNotes = function () {
  fetch(uri)
    .then((response) => response.json())
    .then((data) => {
      // get only user notes and shared notes
      const userNotes = data.filter(
        (note) =>
          note.username === currentUser.name ||
          note.sharedWithUsers.find((u) => u.userName === currentUser.name)
      );
      displayNotes(userNotes);
    })
    .catch((error) => console.error("Unable to get items.", error));
};

getCurrentUser().then((user) => {
  // set user before loading notes
  currentUser = user;
  // load notes
  getNotes();
});
