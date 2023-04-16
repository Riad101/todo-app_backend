using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using TodoApp.Models;
using TodoApp.ViewModel;

namespace TodoApp.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    [RoutePrefix("api/TaskAPI")]
    public class TaskAPIController : ApiController
    {

        sampleDBEntities db = new sampleDBEntities();

        [Route("get_task_list")]
        [HttpGet]
        public async Task<dynamic> get_task_list()
        {
            try
            {

                var find_task_list = db.taskDetails.ToList();


                return Ok(new
                {
                    status_code = "200",
                    status_id = 1,
                    status = "success",
                    msg = "success",
                    return_set_01_data = find_task_list
                });
            }
            catch (Exception ex)
            {
                
                return Json(new { status_code = "404", status_id = "0", status = "error", msg = "error", return_set_01_data = ex });
            }

        }

        [Route("get_task_list_by_taskID")]
        [HttpGet]
        public async Task<dynamic> get_task_list_by_taskID(int taskID)
        {
            try
            {

                var find_task_list = db.taskDetails.Where(x => x.taskID == taskID).Select(s => s).FirstOrDefault();


                return Ok(new
                {
                    status_code = "200",
                    status_id = 1,
                    status = "success",
                    msg = "success",
                    return_set_01_data = find_task_list
                });
            }
            catch (Exception ex)
            {

                return Json(new { status_code = "404", status_id = "0", status = "error", msg = "error", return_set_01_data = ex });
            }

        }


        [Route("create_task")]
        [HttpPost]
        public async Task<dynamic> create_task([FromBody] CreateTaskViewModel frombody )
        {
            try
            {
                


               var find_task_list = db.taskDetails.Select(s=> s).FirstOrDefault();
                
                taskDetails insert_task = new taskDetails
                {
                    taskTitle = frombody.taskTitle,
                    taskDescription = frombody.taskDescription,
                    taskStatus = "New",
                    createdDate = DateTime.Now
                    
                };
                db.taskDetails.Add(insert_task);
                db.SaveChanges();
                
                var return_add_task_list = db.taskDetails.Select(s=>s).ToList();

                return Ok(new
                {
                    status_code = "200",
                    status_id = 1,
                    status = "success",
                    msg = "success",
                    return_set_01_data = return_add_task_list
                });
            }
            catch (Exception ex)
            {

                return Json(new { status_code = "404", status_id = "0", status = "error", msg = "error", return_set_01_data = ex });
            }

        }

        [Route("update_task")]
        [HttpPut]
        public async Task<dynamic> update_task(int taskID, [FromBody] UpdateTaskViewModel frombody)
        {
            try
            {

                var update_task_list = db.taskDetails.Where(x=> x.taskID == taskID).FirstOrDefault();

                if(update_task_list != null)
                {
                    if(frombody.taskTitle != null)
                    {
                        update_task_list.taskTitle = frombody.taskTitle;
                    }

                    if(frombody.taskDescription != null)
                    {
                        update_task_list.taskDescription = frombody.taskDescription;
                    }
                    
                    if(frombody.taskStatus !=null)
                    {

                         update_task_list.taskStatus = frombody.taskStatus;
                    }
                   
                    update_task_list.modifiedDate = DateTime.Now;
                    db.SaveChanges();
                }

                var return_update_task_list = db.taskDetails.Select(S => S).ToList();


                return Ok(new
                {
                    status_code = "200",
                    status_id = 1,
                    status = "success",
                    msg = "success",
                    return_set_01_data = return_update_task_list
                });
            }
            catch (Exception ex)
            {

                return Json(new { status_code = "404", status_id = "0", status = "error", msg = "error", return_set_01_data = ex });
            }

        }

        [Route("Delete_task")]
        [HttpDelete]
        public async Task<dynamic> delete_task(int taskID)
        {
            try
            {

                var delete_task_list = db.taskDetails.Where(x => x.taskID == taskID).FirstOrDefault();

                if (delete_task_list != null)
                {                                        
                    db.taskDetails.Remove(delete_task_list);
                    db.SaveChanges();
                }

                var return_deletee_task_list = db.taskDetails.ToList();


                return Ok(new
                {
                    status_code = "200",
                    status_id = 1,
                    status = "success",
                    msg = "success",
                    return_set_01_data = return_deletee_task_list
                });
            }
            catch (Exception ex)
            {

                return Json(new { status_code = "404", status_id = "0", status = "error", msg = "error", return_set_01_data = ex });
            }

        }
    }
}
