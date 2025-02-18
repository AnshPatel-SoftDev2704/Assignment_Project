import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
  DialogFooter,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { useSelector } from 'react-redux';
import { deleteSubmittedDocument } from "@/services/Document/api";
import { toast } from "react-toastify";

const DeleteDocument = ({ showDeleteDialog, setShowDeleteDialog, deleteDocument }) => {
  const data = useSelector((state) => state.Userdata);

  const validateDocument = () => {
      if (!deleteDocument) {
          toast.error("No document selected for deletion", {
              position: "top-right",
              autoClose: 3000,
              hideProgressBar: false,
          });
          return false;
      }

      if (!deleteDocument.document_id) {
          toast.error("Invalid document ID", {
              position: "top-right",
              autoClose: 3000,
              hideProgressBar: false,
          });
          return false;
      }

      if (!data[0]?.token) {
          toast.error("Authentication token missing", {
              position: "top-right",
              autoClose: 3000,
              hideProgressBar: false,
          });
          return false;
      }

      return true;
  };

  const handleDelete = async () => {
      try {
          if (!validateDocument()) {
              return;
          }

          const response = await deleteSubmittedDocument(data[0].token, deleteDocument.document_id);

          if (response.status === 200) {
              toast.success("Document deleted successfully", {
                  position: "top-right",
                  autoClose: 3000,
                  hideProgressBar: false,
              });
              setShowDeleteDialog(false);
          } else {
              throw new Error("Failed to delete document");
          }
      } catch (error) {
          toast.error(error.message || "Failed to delete document", {
              position: "top-right",
              autoClose: 3000,
              hideProgressBar: false,
          });
      }
  };

  return (
      <Dialog open={showDeleteDialog} onOpenChange={setShowDeleteDialog}>
          <DialogContent className="bg-white text-black p-0 overflow-hidden">
              <DialogHeader className="pt-8 px-6">
                  <DialogTitle className="text-2xl text-center font-bold">
                      Delete Document
                  </DialogTitle>
                  <DialogDescription className="text-center text-zinc-500">
                      Are you sure you want to do this? <br />
                      {deleteDocument?.candidate_Details?.candidate_name ? (
                          <span className="text-indigo-500 font-semibold">
                              {deleteDocument.candidate_Details.candidate_name}'s
                          </span>
                      ) : (
                          <span className="text-red-500">Unknown candidate's</span>
                      )} Document will be permanently deleted.
                  </DialogDescription>
              </DialogHeader>

              <DialogFooter className="bg-gray-100 px-6 py-4">
                  <div className="flex items-center justify-between w-full">
                      <Button
                          onClick={() => setShowDeleteDialog(false)}
                          variant="ghost"
                      >
                          Cancel
                      </Button>
                      <Button
                          variant="primary"
                          onClick={handleDelete}
                          className="bg-red-500 hover:bg-red-600 text-white"
                      >
                          Confirm Delete
                      </Button>
                  </div>
              </DialogFooter>
          </DialogContent>
      </Dialog>
  );
};

export default DeleteDocument;