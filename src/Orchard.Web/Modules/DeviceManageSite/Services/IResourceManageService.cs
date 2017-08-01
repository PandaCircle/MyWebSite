using DeviceManageSite.Models;
using Orchard;
using Orchard.Data;
using Orchard.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceManageSite.Services
{
    public interface IResourceManageService:IDependency
    {
        int Remians(string type);
        ClassifyRecord NewClassify(string clsName, ResourceTypeRecord resType);
        ClsResRecord ClassifyResource(int clsId, ResourceRecord res);
        void RemoveClassifyResource(int resId,string clsName);
        void RemoveClassifyResource(int[] resIds, string clsName);
        void DeleteClassify(int clsId);
        IEnumerable<ResourceTypeRecord> ResourceTypes();
        ResourceTypeRecord GetResTypeByName(string resType);
        ResourceTypeRecord GetResTypeById(int id);
        ClassifyRecord GetClsByName(string clsName);
        ClassifyRecord GetClsById(int id);
        IEnumerable<ResourceRecord> GetClssifiedResources(int clsId);
    }

    public class ResourceManageService : IResourceManageService
    {
        private readonly IRepository<ResourceRecord> _resourceRepository;
        private readonly IRepository<ResourceTypeRecord> _resTypeRepository;
        private readonly IRepository<ClassifyRecord> _resClassifyRepository;
        private readonly IRepository<ClsResRecord> _clsResRepository;
        private readonly IAuthorizationService _authorizationService;

        public ResourceManageService
            (
                IRepository<ResourceRecord> resoruceRepository,
                IRepository<ResourceTypeRecord> resTypeRepository,
                IRepository<ClassifyRecord> resClassifyRepository,
                IRepository<ClsResRecord> clsResRepository,
                IAuthorizationService authorizationService,
                IOrchardServices orchardService

            )
        {
            _resourceRepository = resoruceRepository;
            _resTypeRepository = resTypeRepository;
            _resClassifyRepository = resClassifyRepository;
            _clsResRepository = clsResRepository;
            _authorizationService = authorizationService;
            OrchardService = orchardService;
        }

        public IOrchardServices OrchardService { get; set; }

        public ResourceTypeRecord GetResTypeById(int id)
        {
            return _resTypeRepository.Get(id);
        }

        public ResourceTypeRecord GetResTypeByName(string resType)
        {
            return _resTypeRepository.Get(i => i.DisplayName == resType);
        }

        public ClassifyRecord NewClassify(string clsName, ResourceTypeRecord resType)
        {
            var result = _resClassifyRepository.Get(i => i.ClsName == clsName);
            if(result == null)
            {
                result = new ClassifyRecord { ClsName = clsName, ResourceType = resType };
                _resClassifyRepository.Create(result);
            }
            return result;
        }

        public int Remians(string type)
        {
            return _resourceRepository.Table.Fetch(i => i.ResourceType.DisplayName == type && i.AttachUnit == 0).Count();
        }

        public IEnumerable<ResourceTypeRecord> ResourceTypes()
        {
            return _resTypeRepository.Table;
        }

        public ClassifyRecord GetClsByName(string clsName)
        {
            return _resClassifyRepository.Get(i => i.ClsName == clsName);
        }

        public ClassifyRecord GetClsById(int id)
        {
            return _resClassifyRepository.Get(id);
        }

        public void DeleteClassify(int clsId)
        {
            _authorizationService.CheckAccess(Permissions.ResourceAdmin, OrchardService.WorkContext.CurrentUser, null);
            var cls = GetClsById(clsId);
            if (cls == null)
                return;
            _resClassifyRepository.Delete(cls);
        }

        public IEnumerable<ResourceRecord> GetClssifiedResources(int clsId)
        {
            return _resClassifyRepository.Get(clsId).ClassifyResourceRecords.Select(i => i.Resource);
        }

        public ClsResRecord ClassifyResource(int clsId, ResourceRecord res)
        {
            var clsResRecord = res.Classes.Where(i => i.Classify.Id == clsId).SingleOrDefault();
            if(clsResRecord == null)
            {
                clsResRecord = new ClsResRecord { Resource = res, Classify = GetClsById(clsId) };
                _clsResRepository.Create(clsResRecord);
            }
            return clsResRecord;
        }

        public void RemoveClassifyResource(int resId, string clsName)
        {
            foreach(var record in _clsResRepository.Fetch(i=>i.Classify.ClsName == clsName && i.Resource.Id == resId))
            {
                _clsResRepository.Delete(record);
            }
        }

        public void RemoveClassifyResource(int[] resIds, string clsName)
        {
            foreach(var rid in resIds)
            {
                RemoveClassifyResource(rid, clsName);
            }
        }
    }
}