import { gql } from '@apollo/client';

export const ValidateUserStoryGroupMutation = gql`
  mutation ValidateUserStoryGroup($projectId: Int!, $input: Object!) {
    group(projectId: $projectId, id: $id, input: $input)
      @rest(
        type: "IUserStoryGroupDto"
        method: "PUT"
        path: "/project/{args.projectId}/group/{args.id}/validate"
      ) {
      id
      projectId
      request
      response
      userStories
    }
  }
`;
