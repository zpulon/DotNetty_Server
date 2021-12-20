
using Authority.Controllers.Models;
using Authority.Service;
using DotNetty_Common;
using DotNetty_ControllerBus;
using DotNetty_ControllerBus.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authority.Controllers
{

    public class CommentController : BaseController
    {
        private readonly IChatSessionService _ichatSessionService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ichatSessionService"></param>
        /// <param name="connections"></param>
        public CommentController(IChatSessionService ichatSessionService)
        {
            _ichatSessionService = ichatSessionService;
        }


        /// <summary>
        /// 进入直播间到redis 获取信息
        /// </summary>
        /// <param name="request">用户信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResultModel<RedisMessage>> GetMessageList(ChatRequest request)
        {
            PageResultModel<RedisMessage> resultModel = new PageResultModel<RedisMessage> { Data = new List<RedisMessage>()}; 
            try
            {
                
                resultModel.Data = await _ichatSessionService.GetMessageList(request.ClassRoomId, request.PageIndex, request.PageSize, request.desc);
                resultModel.PageIndex = request.PageIndex;
                resultModel.PageSize = request.PageSize;
                resultModel.TotalCount = await _ichatSessionService.GetMessageCount(request.ClassRoomId);
            }
            catch (DotNettyServerException exception)
            {

                ConsoleHelper.ServerWriteError(exception);
            }
            return resultModel;

        }
        /// <summary>
        /// 进入直播间到redis 获取单条信息
        /// </summary>
        /// <param name="classRoomId"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel<RedisMessage>> GetMessage(MessageRequest request)
        {
            ResultModel<RedisMessage> response = new();
            try
            {
                response.Data = await _ichatSessionService.GetMessageAsync<RedisMessage>(request.ClassRoomId, request.Score);
            }
            catch (DotNettyServerException exception)
            {
                ConsoleHelper.ServerWriteError(exception);

            }
            return response;

        }
        /// <summary>
        /// 获取在线人数   
        /// </summary>
        /// <param name="classRoomId">教室标识</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<long>> GetClassRoomNumber(string classroomid)
        {
            var result = new ResultModel<long>();
            try
            {
                var number = await _ichatSessionService.HashStringLength(classroomid);
                result.Data = number;
            }
            catch (DotNettyServerException exception)
            {
                ConsoleHelper.ServerWriteError(exception);
    
            }
            return result;
        }

    }
}
