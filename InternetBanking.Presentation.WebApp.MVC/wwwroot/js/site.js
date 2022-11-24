let arrow = document.querySelectorAll(".arrow");
for (var i = 0; i < arrow.length; i++) {
    arrow[i].addEventListener("click", (e) => {
        let arrowParent = e.target.parentElement.parentElement;//selecting main parent of arrow
        arrowParent.classList.toggle("showMenu");
    });
}

let sidebar = document.querySelector(".sidebar");
let sidebarBtn = document.querySelector(".bxs-bank");
console.log(sidebarBtn);
sidebarBtn.addEventListener("click", () => {
    sidebar.classList.toggle("close");
});

let restorePassBtn = document.querySelector("#add-beneficiary");

restorePassBtn.addEventListener("click", async () => {
    const { value: accountNumber } = await Swal.fire({
        title: "Input the account number to register a new beneficiary",
        html: `<form method="post" action="Beneficiaries/Index" id="frm-add-beneficiary"> 
          <input id="content" type="text" class="form-control border-secondary border border-2" placeholder="Enter the account number" name="RecipientCode" required>
          </form>`,
        showCancelButton: true,
        focusConfirm: false,
        preConfirm: () => {
            return [document.getElementById("content").value];
        },
    });

    if (accountNumber) {
        if (accountNumber.filter(Boolean).length < 1) {
            Swal.fire("Error!", "The field account number can't be empty", "error");
        } else {
            let form = document.querySelector("#frm-add-beneficiary");
            form.submit();
        }
    }
});


const montoInput = document.querySelector("#monto");
const montoLabel = document.querySelector("#montoLabel");
const rolSelect = document.querySelector("#rol-select");

document.addEventListener('DOMContentLoaded', () => {

    montoLabel.classList.add("d-none");
    montoInput.setAttribute("type", "hidden");

    rolSelect.onchange = (e) => {

        if (e.target.value === "1") {
            montoLabel.classList.add("d-none");
            montoInput.setAttribute("type", "hidden");
        } else {
            montoLabel.classList.remove("d-none");
            montoInput.setAttribute("type", "text");
        }
    }
});